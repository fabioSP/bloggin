<frame id="detail-page-frame">
	<page>
    <actionBar title="Cadastre-se"></actionBar>
    <stackLayout>
      <textField hint="Nome" bind:text="{name}" keyboardType="text" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <textField hint="E-mail" bind:text="{email}" keyboardType="email" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
      <textField hint="Senha" secure="true" bind:text="{password}" class="input input-border" />
      <textField hint="Repita a Senha" secure="true" bind:text="{repassword}" class="input input-border" />
      <flexboxLayout flexDirection="column" class="p-t-25">
        <button text="Cadastrar" on:tap="{ () => register() }" />
        <button text="Cancelar" on:tap="{ () => closeModal(false) }" />
      </flexboxLayout>
    </stackLayout>
	</page>
</frame>

<script>

  import { closeModal } from 'svelte-native'
  import * as Utils from '../../Utils/functions'
  const firebase = require("nativescript-plugin-firebase");

  let name;
  let email;
  let password;
  let repassword;

  async function register()
  {
    if(!email || !Utils.ValidateEmail(email))
    {
      alert({
        title: "Atenção",
        message: "Por favor digite um e-mail válido.",
        okButtonText: "Ok"
      });
    } else if(!password || !repassword) {
      alert({
        title: "Atenção",
        message: "Por favor digite uma senha.",
        okButtonText: "Ok"
      });
    } else if(password !== repassword) {
      alert({
        title: "Atenção",
        message: "As senhas não correspondem.",
        okButtonText: "Ok"
      });
    } else {
      await firebase.createUser({ email: email, password: password })
      .then(() =>{
        firebase.updateProfile({ displayName: name })
        alert({
          title: "Atenção",
          message: "Cadastro realizado com sucesso!",
          okButtonText: "Ok"
        }).then(function (){
          closeModal(true);
        });
      })
      .catch(function(error) {
        alert({
          title: "Erro",
          message: `Ocorreu um erro ao realizar o cadastro. [${error}]`,
          okButtonText: "Ok"
        }).then(function (){
          closeModal(false);
        });
      });
    }
  }

</script>