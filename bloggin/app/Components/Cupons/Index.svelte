<page>
  <actionBar title="Cupons" height="120" flat="false">
    <navigationButton text="Go back" android.systemIcon="ic_menu_back" on:tap="{goBack}" height="10"/>
  </actionBar>
  <stackLayout class="p-l-25 p-t-20">
    <label text="add_box" class="icon_ mdi" />
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
  .icon_ {
    font-size: 35pt;
  }
</style>