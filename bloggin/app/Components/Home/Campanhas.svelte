<script>
  import Cadastro from './Campanhas/Cadastro.svelte'
  import { showModal } from 'svelte-native'
  import * as Store from '~/Store/Index'
  import CadastroPublis from './Campanhas/CadastroPublis.svelte';

  const firebase = require('nativescript-plugin-firebase')
  let user;

  Store.LoginStore.subscribe(value => {
    user = value.uid;
  });

  async function registerCampaign() {
    await showModal({ page: Cadastro }).then(() => {
      loadCampaigns();
    })
  }

  async function createPubli(campaignID) {
    await showModal({ page: CadastroPublis, props: { campaignID: campaignID } })
    .then(result => {
      if(result) loadCampaigns()
    })
  }

  let listOfItems = [];
  let partner;

  async function loadPartners() {
    partner = await firebase.firestore
      .collection(`parceiros`).where('userID', '==', user).get()
      .then(values => {
       if(values.empty) {
         return '';
       } else {
         return values.docs.map(e => {
           return { id: e.id, data: e.data() }
         })
       }
      });
  }

  loadPartners();

  async function loadCampaigns() {
    let campaigns = await firebase.firestore
      .collection(`campanhas`).where('userID', '==', user).get()
      .then(values => {
       if(values.empty) {
         return [];
       } else {
         return values.docs.map(e => {
           return { id: e.id, data: e.data() }
         })
       }
      });
    listOfItems = campaigns;
    for(let i = 0; i < listOfItems.length; i++) {
      listOfItems[i]['parceiro'] = partner.find(e => e.id === listOfItems[i].data.parceiroID).data.name
    }
  }

  loadCampaigns();

</script>
<stackLayout class="main_content">
  <dockLayout class="section" stretchLastChild="false">
    <wrapLayout dock="top" class="container_campaign">
      <scrollView dock="top" orientation="vertical" scrollBarIndicatorVisible="false">
        <stackLayout orientation="vertical">
          {#each listOfItems as item}
            <flexboxLayout class="campaign" flexDirection="column">
              <label text="{item.data.name}" class="capmaignTitle" />
              <label text="Parceiro: {item.parceiro}" />
              <label text="Publis: {item.data.publis.length}" />
              <wrapLayout on:tap={() => createPubli(item.id)}>
                <label text="add_box" class="icon_ mdi" />
                <label text="Nova publi" verticalAlignment="center"/>
              </wrapLayout>
            </flexboxLayout>
          {/each}
        </stackLayout>  
      </scrollView>
    </wrapLayout>
    <button text="Nova campanha" class="btn_new_campaign" dock="bottom" on:tap="{() => registerCampaign()}" />
  </dockLayout>
</stackLayout>

<style>
  .icon_ {
    font-size: 35pt;
  }
  .capmaignTitle {
    font-size: 25pt;
    font-weight: bold;
    text-align: center;
  }
  .main_content {
    margin-left: 30;
    margin-right: 30;
  }
  .section {
    display: flex;
    flex-direction: column;
    height: 100%;
  }
  .campaign {
    color: white;
    font-size: 15;
    background-color: #555;
    border-radius: 12;
    padding: 10 10;
    height: 200;
    width: 100%;
    margin-top: 20px;
  }
  .btn_new_campaign {
    margin-bottom: 10px;
    height: 10%;
    width: 100%;
    border-radius: 8%;
    background-color: #524e91;
    color:white;
  }
  .container_campaign {
    height: 88%;
  }

</style>