  <gridLayout rows="*, auto" columns="250, *">
    <label text="This is Label in row 0, col 0" />
    <label text="This is Label in row 0, col 1" col="1" />
    <label text="This is Label in row 1, col 0" row="1" />
    <label text="This is Label in row 1, col 1" row="1" col="1" />
    <label text="This is Label in row 0, col 0" rowSpan="2" colSpan="2" />
  </gridLayout>

<script>
  import { text } from 'svelte/internal'


</script>
