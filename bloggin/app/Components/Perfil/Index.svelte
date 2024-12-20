<page>
  <actionBar title="Perfil" height="120" flat="false">
    <navigationButton text="Go back" android.systemIcon="ic_menu_back" on:tap="{goBack}" height="10"/>
  </actionBar>
  <stackLayout>
    <label text="Perfil" />
  </stackLayout>
</page>

<script>
  import { goBack } from 'svelte-native'
</script>

<style>
  actionBar {
    background-color: #ffffff;
    color: black;
    font-size: 30pt;
    font-weight: bold;
  }
</style>