<frame id="detail-page-frame">
	<page>
    <actionBar title="Novo parceiro"></actionBar>
    <stackLayout>
      <textField hint="Nome" bind:text="{name}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <textField hint="Responsável" bind:text="{ownerName}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <textField hint="Telefone" bind:text="{phone}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <textField hint="E-mail" bind:text="{email}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <flexboxLayout flexDirection="column" class="p-t-25">
        <button text="Cadastrar" on:tap="{ () => createPartner() }" />
        <button text="Cancelar" on:tap="{ () => closeModal(false) }" />
      </flexboxLayout>
    </stackLayout>
	</page>
</frame>

<script>
  import { closeModal } from 'svelte-native'
  import * as Utils from '~/Utils/functions'
  import * as Store from '~/Store/Index'

  const firebase = require('nativescript-plugin-firebase')

  let name;
  let ownerName;
  let phone;
  let email;
  let user;

  Store.LoginStore.subscribe(value => {
    user = value.uid
  });

  let db = firebase.firestore.collection('parceiros').doc(Utils.NewGuid());

  function createPartner() {
    if(!Utils.ValidateEmail(email)) {
      alert({
        title: "Atenção",
        message: "Por favor digite um e-mail válido.",
        okButtonText: "Ok"
      });
    } else if (!name || !ownerName || !phone) {
      alert({
        title: "Atenção",
        message: "Por favor preencha todos os campos.",
        okButtonText: "Ok"
      });
    } else {
      db.set({
        'name': name,
        'ownerName': ownerName,
        'phone': phone,
        'email': email,
        'userID': user
      }).then((result) => {
        console.log("funciona")
      }).catch((error) => {
        alert(`Ocorreu um erro: [${error}]`);
      });

      closeModal(true);
    }
  }

</script>