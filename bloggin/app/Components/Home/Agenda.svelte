  <gridLayout height="1000" columns="*,*,*" rows="60,*,*,*,*" verticalalignment="top">
    <label col="0" class="lbl_calen" text="Hoje" />
    <scrollView row="1" col="0" colSpan="3" height="200" class="day_items" verticalAlignment="center" scrollBarIndicatorVisible="false" orientation="horizontal">
      <stackLayout orientation="horizontal">
        {#each itemsToday as item}
          <stackLayout class="day_item">
            <label text="{item.campaign}" />
            <label text="Canal: {item.canal}" />
            <label text="Data: {item.data}" />
          </stackLayout>
        {/each}
      </stackLayout>
    </scrollView>
    <label row="2" col="0" class="lbl_calen p-t-30" colSpan="3" text="Semana" verticalalignment="center"/>
    <label row="3" col="0" colSpan="3" rowSpan="2" class="week_item" text="if necessary" verticalalignment="top"/>
  </gridLayout>

<script>
import * as Store from '~/Store/Index'

  const firebase = require('nativescript-plugin-firebase')
  let user;
  let itemsToday = [];

  Store.LoginStore.subscribe(value => {
    user = value.uid
  });

  async function loadSchedule() {
    await firebase.firestore
      .collection(`campanhas`).where('userID', '==', user).get()
      .then(values => {
       if(values.empty) {
         return [];
       } else {
         return values.docs.map(e => {
           return { id: e.id, data: e.data() }
         })
       }
      }).then(ret => {
        loadItemsToday(ret)
      })
  }

  loadSchedule();

  function loadItemsToday(info) {
    info.map(data => { 
      if(data.data.publis.length > 0) {
        let d = new Date();
        let todayPublis = data.data.publis.filter(e => e.data == `${d.getDate()}/${d.getMonth()+1}/${d.getFullYear()}`);
        itemsToday = todayPublis.map(t => ({ campaign: data.data.name, canal: t.canal, data: t.data }))
      }
    });
  }

</script>

<style>
  .week_item {
    color: white;
    font-size: 15;
    background-color: #555;
    border-radius: 12;
    padding: 5 5;
    margin-left: 30;
    margin-right: 30;
    height: 200;
  }

  .lbl_calen {
    font-size: 30;
    color: black;
    margin-left: 30;
  }

  .day_items {
    margin-left: 30;
    margin-right: 30;
  }

  .day_item {
    color: white;
    font-size: 15;
    background-color: #555;
    border-radius: 12;
    padding: 5 5;
    margin-right: 10;
    width: 150;
  }
  </style>