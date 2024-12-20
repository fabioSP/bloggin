<page actionBarHidden="true">
  <radSideDrawer bind:this="{side_drawer}">
    <radSideDrawer.drawerContent>
      <Menu />
    </radSideDrawer.drawerContent>
    <radSideDrawer.mainContent>
      <stackLayout class="main">
        <frame>
          <page>
            <actionBar height="120" flat="true">
              <gridLayout columns="auto, *" rows="*">
                <label row="0" col="0" text="menu" class="mdi menuSize" horizontalAlignment="left"
                  verticalAlignment="top" on:tap="{() => side_drawer.nativeView.toggleDrawerState() }" ></label>
                <label row="0" col="0" verticalAlignment="top" text=" {name} " class="usr_name" horizontalAlignment="right"></label>
                <absoluteLayout class="p-b-2 p-l-10 p-t-5" verticalAlignment="bottom">
                  <wrapLayout horizontalAlignment="center" class="tags">
                    <label text="Agenda" textWrap="true" on:tap={() => handleChangeTab(0)} class=" {tabSelected.Option == 0 ? 'tag_active' : 'tag'} "/>
                    <label text="Campanhas" textWrap="true" on:tap={() => handleChangeTab(1)} class=" {tabSelected.Option == 1 ? 'tag_active' : 'tag'} "/>
                    <label text="Parceiros" textWrap="true" on:tap={() => handleChangeTab(2)} class=" {tabSelected.Option == 2 ? 'tag_active' : 'tag'} "/>
                  </wrapLayout>
                </absoluteLayout>
              </gridLayout>
            </actionBar>
            <stackLayout class="content">
              <svelte:component this={tabSelected.Component} />
            </stackLayout>
          </page>
        </frame>
      </stackLayout>
    </radSideDrawer.mainContent>
  </radSideDrawer>
</page>

<script>
  import Menu from '../Main/Menu.svelte'
  import Agenda from './Agenda.svelte'
  import Campanhas from './Campanhas.svelte'
  import Parceiros from './Parceiros.svelte'
  import { registerNativeViewElement } from 'svelte-native/dom'
  import * as Store from '~/Store/Index'

  registerNativeViewElement("radSideDrawer", () => require("nativescript-ui-sidedrawer").RadSideDrawer)

  let name;

  Store.LoginStore.subscribe(value => {
    name = value.displayName
  });

  let side_drawer;
  
  let tabsContent = [
    { Option: 0, Component: Agenda },
    { Option: 1, Component: Campanhas },
    { Option: 2, Component: Parceiros }
  ];

  let tabSelected = tabsContent[0];

  function handleChangeTab(index) {
    tabSelected = tabsContent[index];
  }

</script>

<style>
  .usr_name {
    color: #ffffff;
    text-decoration: underline;
  }

  .menuSize {
    font-size: 30pt;
    color: #ffffff;
  }

  .tags {
    padding: 0 10;
  }

  .tag {
    color: white;
    font-size: 15;
    padding: 5 10;
    margin: 15;
  }

  .tag_active {
    color: #000000;
    font-size: 15;
    background-color: #ffffff;
    border-radius: 12;
    padding: 5 10;
    margin: 15;
  }

  .content {
    border-top-right-radius: 25;
    border-top-left-radius: 25;
    background-color: #ffffff;
  }
  page {
    background-color: #6121b5;
  }
  </style>
