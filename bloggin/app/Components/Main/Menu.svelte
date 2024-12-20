<script>
  import { navigate } from 'svelte-native'
  import { onMount } from 'svelte';
  import Perfil from '../Perfil/Index.svelte'
  const firebase = require('nativescript-plugin-firebase')
  // import Orcamentos from '../Orcamentos/Index.svelte'
  // import Pagamentos from '../Pagamentos/Index.svelte'
  // import Cupons from '../Cupons/Index.svelte'
  // import IndiqueAmigos from '../IndiqueAmigos/Index.svelte'
  import Login from '../Login/Index.svelte'
  import Configuracoes from '../Configuracoes/Index.svelte'
  import * as Store from '~/Store/Index'

  let pages = [
    { Option: 0, Page: Perfil },
    { Option: 1, Page: null },
    // { Option: 2, Page: Orcamentos },
    // { Option: 3, Page: Pagamentos },
    // { Option: 4, Page: Cupons },
    // { Option: 5, Page: IndiqueAmigos },
    { Option: 6, Page: Configuracoes },
  ]
  let name;
  Store.LoginStore.subscribe(value => {
    name = value.displayName
  })

  export let itemSelected = pages[1];

  function handleItemChange (index) {
    itemSelected = pages[index];
    if (index !== 1) navigate({ page: itemSelected.Page })
  }

  function logoff() {
    firebase.logout();
    navigate({ page: Login })
  }

  onMount(() => {
    itemSelected = pages[1];
  })

</script>
<stackLayout class="drawerContent">
  <stackLayout orientation="horizontal" class="p-t-20">
    <image src="~/Assets/images/logo.png" horizontalAlignment="center" width="300" height="30" />
  </stackLayout>
  <flexboxLayout class="p-t-30" flexDirection="column" justifyContent="space-around" horizontalAlignment="center">
    <stackLayout orientation="horizontal" class="b_account" width="250" on:tap={ () => handleItemChange(0) }>
      <label text="face" class="mdi b_avatar" />
      <label text="{name}" class="p-l-10" verticalAlignment="center"/>
      <label text="keyboard_arrow_right" class="mdi b_avatar_arrow p-l-30" verticalAlignment="center"/>
    </stackLayout>
  </flexboxLayout>
  <flexboxLayout class="p-t-30" flexDirection="column" justifyContent="space-around" horizontalAlignment="left">
    <stackLayout on:tap={() => handleItemChange(1)} class="b_menu_item_active" orientation="horizontal" width="260">
      <label text="home" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Início" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
    <!-- svelte-ignore <stackLayout on:tap={() => handleItemChange(2)} class="p-t-20 b_menu_item" orientation="horizontal" width="260">
      <label text="receipt" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Orçamentos" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
    <stackLayout on:tap={() => handleItemChange(3)} orientation="horizontal" class="p-t-20 b_menu_item" width="260">
      <label text="monetization_on" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Pagamentos" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
    <stackLayout on:tap={() => handleItemChange(4)} orientation="horizontal" class="p-t-20 b_menu_item" width="260">
      <label text="redeem" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Cupons" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
    <stackLayout on:tap={() => handleItemChange(5)} orientation="horizontal" class="p-t-20 b_menu_item" width="260">
      <label text="favorite" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Indique amigos" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>-->
    <stackLayout on:tap={() => handleItemChange(2)} orientation="horizontal" class="p-t-20 b_menu_item" width="260">
      <label text="settings" class="mdi p-l-20 b_menu_item_icon"/>
      <label text="Configurações" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
    <stackLayout on:tap={() => logoff()} orientation="horizontal" class="p-t-20" width="260">
      <label text="exit_to_app" class="mdi p-l-20 b_menu_item_icon" verticalAlignment="stretch"/>
      <label text="Sair" class="p-l-10" verticalAlignment="center"/>
    </stackLayout>
  </flexboxLayout>
</stackLayout>

<style>
  .drawerContent {
    background-color:#ffffff;
    border-top-right-radius: 25;
    border-bottom-right-radius: 25;
  }
  .b_account {
    color: white;
    background-color: #555;
    border-radius: 50;
    width: 70;
  }
  .b_avatar {
    font-size: 50vw;
  }
  .b_avatar_arrow {
    font-size: 40vw;
  }
  .b_menu_item_icon {
    color: #000000;
    font-size: 30vw;
  }

  .b_menu_item {
    padding-top: 5;
    padding-bottom: 5;
  }

  .b_menu_item_active {
    background-color: cadetblue;
    border-top-right-radius: 25;
    border-bottom-right-radius: 25;
    padding-top: 5;
    padding-bottom: 5;
  }
</style>