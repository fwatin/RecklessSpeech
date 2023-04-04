<script>
import axios from "axios";
export default {
  setup() {},
  data() {
    return {
      filePickerDialog: false,
      words: [],
    };
  },
  mounted() {
    axios
      .get("https://localhost:47973/api/v1/sequences")
      .then((response) => {
        this.words = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  methods: {
    openFilePicker() {
      this.filePickerDialog = true;
    },
  },
};
</script>
<template>
  <div>
    <div>
      <b-button class="clickable" @click="openFilePicker"
        >Import items.csv</b-button
      >
    </div>

    <div>
      <b-table>
        <b-table-simple>Cras justo odio</b-table-simple>
      </b-table>
    </div>
  </div>
</template>


<style scoped>
.clickable {
  cursor: pointer;
}
</style>