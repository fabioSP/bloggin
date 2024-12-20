<frame id="detail-page-frame">
	<page>
    <actionBar title="Nova publi"></actionBar>
    <stackLayout>
      <textField hint="{publiDate}" bind:text="{publiDate}" on:tap="{() => pickDate()}" editable="false"></textField>
      <textField hint="{selectedChannel}" bind:text="{selectedChannel}" on:tap="{() => showDialog()}" editable="false"></textField>
      <flexboxLayout flexDirection="column" class="p-t-25 btns">
        <button text="Cadastrar" on:tap="{ () => registerPubli() }" />
        <button text="Cancelar" on:tap="{ () => closeModal(false) }" />
      </flexboxLayout>
    </stackLayout>
	</page>
</frame>

<script>
  import { closeModal } from 'svelte-native'
  import * as ModalPicker from 'nativescript-modal-datetimepicker';

  export let campaignID;

  const firebase = require('nativescript-plugin-firebase')

  let db = firebase.firestore.collection('campanhas').doc(campaignID);

  function testDate(dateString) {
    const userKeyRegExp = /^[0-9]{0,2}\/[0-9]{0,2}\/[0-9]{4}?$/;
    return userKeyRegExp.test(dateString);
  }

  async function registerPubli() {
    if(!testDate(publiDate) || channels.filter(c => c === selectedChannel).length !== 1) {
      alert("Existem campos preenchidos incorretamente.");
      
    } else {
      await db.update({
        publis: firebase.firestore.FieldValue.arrayUnion({ data: publiDate, canal: selectedChannel })
      }).then((result) => {
        closeModal(true);
      }).catch((error) => {
        alert(`Ocorreu um erro: [${error}]`);
      });
    }
  }

  let publiDate = "Escolha a data da publicação.";
  let selectedChannel = "Rede social";

  let channels = ["Instagram", "Facebook", "Twitter"];

  var options = {
    title: "Canal de publicação",
    message: "Escolha por onde será realizada a publicação.",
    cancelButtonText: "Cancelar",
    actions: channels 
  };

  function showDialog() {
    action(options).then((result) => {
      selectedChannel = result !== 'Cancelar' ? result : "Rede social";
    });
  }

  function pickDate() {
    const picker = new ModalPicker.ModalDatetimepicker();
    picker.pickDate({
      title: 'Escolha a data de divulgação',
      theme: 'dark',
      minDate: new Date(),
      is24HourView: false
    }).then((result) => {
      publiDate = `${result['day']}/${result['month']}/${result['year']}`;
    }).catch((error) => {
      console.log('Error: ' + error);
    });
  }
</script>