<page>
  <actionBar height="250" flat="false">
    <image src="~/Assets/images/logoiconbkg.png" horizontalAlignment="center" width="400" height="150" />
  </actionBar>
  <flexboxLayout>
    <stackLayout class="form">
        <textField hint="E-mail" bind:text="{_email}" keyboardType="email" autocorrect="false"
            autocapitalizationType="none" class="input input-border" />
        <textField hint="Senha" secure="true" bind:text="{_password}" class="input input-border" />

        <button text="Entrar" class="btn btn-primary" on:tap="{() => goLogin()}"/>
        <button text="Cadastrar" on:tap="{() => launchModal()}" />

    </stackLayout>
  </flexboxLayout>
</page>

<script>
  const firebase = require("nativescript-plugin-firebase");
  import Cadastro from './Cadastro.svelte'
  import * as Store from '../../Store/Index'
	import { navigate, showModal } from 'svelte-native'
  import Home from '../Home/Index.svelte';


	async function launchModal() {
		let result = await showModal({ page: Cadastro })
	}

  let _email, _password;
  
  async function goLogin(){
    await firebase.login({ type: firebase.LoginType.PASSWORD, passwordOptions: { email: _email, password: _password } })
    .then(function (user) {
      Store.LoginStore.set(user);
      navigate({ page: Home })
    })
    .catch(function(error) {
      alert({
        title: "Erro",
        message: `Ocorreu um erro. [${error}]`,
        okButtonText: "Ok"
      }).then(function (){
        console.log("não funcionou");
      });
    });
  }


</script>

<style>
  actionBar {
    border-bottom-left-radius: 120;
    background-color: #6121b5;
  }
  FlexboxLayout {
    justify-content: center;
    align-items: center;
    background-size: cover;
  }
  StackLayout {
    width: 300;
    padding: 10 16;
  }
  Button,
  TextField {
    margin-bottom: 10;
  }
  .btn-primary {
    background-color: #555;
    color: white;
    margin-left: 0;
    margin-right: 0;
  }
  TextField {
    placeholder-color: #C4AFB4;
    color: black;
  }
</style>