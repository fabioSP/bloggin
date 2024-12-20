<frame id="detail-page-frame">
	<page>
    <actionBar title="Nova campanha"></actionBar>
    <stackLayout>
      <textField hint="Nome da campanha" bind:text="{name}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <listPicker items="{parceirosList}" selectedIndex="0"
        on:selectedIndexChange="{selectedIndexChanged}" textField="text" value="id" />
      <flexboxLayout flexDirection="column" class="p-t-25 btns">
        <button text="Cadastrar" on:tap="{ () => registerCampaign() }" />
        <button text="Cancelar" on:tap="{ () => closeModal(false) }" />
      </flexboxLayout>
    </stackLayout>
	</page>
</frame>

<script>
  import { closeModal } from 'svelte-native'
  import * as Store from '~/Store/Index'
  import * as Utils from '~/Utils/functions'

  const firebase = require('nativescript-plugin-firebase')

  let user;

  Store.LoginStore.subscribe(value => {
    user = value.uid;
  });

  let parceirosList = [];
  let bindValue = [];

  async function loadPartners() {
    let partners = await firebase.firestore
      .collection(`parceiros`).where('userID', '==', user).get()
      .then(values => {
       if(values.empty) {
         return [];
       } else {
         return values.docs.map(e => ({ 'text': e.data().name, id: e.id }))
       }
      });
      parceirosList = partners;
      parceirosList.unshift({ text: "-- Escolha um parceiro --", id: null})
      for(let i = 0; i < parceirosList.length; i++) {
        bindValue.push({ index: i, id: parceirosList[i].id })
      }
  }

  loadPartners();

  let name;
  let parceiro;

  const selectedIndexChanged = (e) => {
    for(let i = 0; i < bindValue.length; i++) {
      if(e.value === bindValue[i].index) {
        parceiro = bindValue[i].id
      }
    }
  }

  Store.LoginStore.subscribe(value => {
    user = value.uid
  });

  let db = firebase.firestore.collection('campanhas').doc(Utils.NewGuid());

  function registerCampaign() {
    db.set({
      'name': name,
      'parceiroID': parceiro,
      'userID': user,
      'publis': []
    }).then((result) => {
      console.log("funciona")
      closeModal(true);
    }).catch((error) => {
      alert(`Ocorreu um erro: [${error}]`);
    });
  }

</script>

<style>
</style>