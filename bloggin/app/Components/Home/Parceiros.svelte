<stackLayout class="main_content">
  <dockLayout class="section" stretchLastChild="false">
    <wrapLayout dock="top" class="container_partners">
      <listView items="{listOfItems}" on:itemTap="{onItemTap}">
        <Template let:item>
          <wrapLayout>
            <label text="{item.name}" class="row_item"/>
            <label text="{item.ownerName.substr(0, item.name.indexOf(" "))}" class="row_item"/>
            <label text="{item.phone}" />
          </wrapLayout>
        </Template>
      </listView>
    </wrapLayout>
    <button text="Novo parceiro" class="btn_new_partner" on:tap="{() => registerPartner()}" />
  </dockLayout>
</stackLayout>

<script>
  import { Template } from 'svelte-native/components'
  import { showModal } from 'svelte-native'
  import Cadastro from './Parceiros/Cadastro.svelte'
  import * as Store from '~/Store/Index'

  const firebase = require('nativescript-plugin-firebase')
  let user;

  Store.LoginStore.subscribe(value => {
    user = value.uid;
  });

  let listOfItems = [];

  async function loadPartners() {
    let partners = await firebase.firestore
      .collection(`parceiros`).where('userID', '==', user).get()
      .then(values => {
       if(values.empty) {
         return [];
       } else {
         return values.docs.map(e => e.data())
       }
      });
    listOfItems = partners;
  }

  loadPartners();

  async function registerPartner() {
    await showModal({ page: Cadastro })
    .then((result) => {
      if(result) {
        loadPartners();
      }
    })
  }
  
  function onItemTap(event) {
    console.log(event.index) //item index
  }
</script>

<style>
  .main_content {
    margin-left: 30;
    margin-right: 30;
  }
  .section {
    display: flex;
    flex-direction: column;
    height: 100%;
  }
  .container_partners {
    height: 88%;
  }
  .row_item {
    margin-right: 15%;
  }
  .btn_new_partner {
    margin-bottom: 10px;
    height: 10%;
    width: 97%;
    border-radius: 8%;
    background-color: #524e91;
    color:white;
  }
</style>