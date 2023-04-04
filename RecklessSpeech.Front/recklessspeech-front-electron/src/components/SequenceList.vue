<script>
import axios from "axios";
export default {
  setup() {},
  data() {
    return {
      filePickerDialog: false,
      words: [],
      checkedWords: [],
    };
  },
  mounted() {
    axios
      .get("https://localhost:47973/api/v1/sequences")
      .then((response) => {
        this.words = response.data;
        console.log(
          this.words.length + " words set into the variable 'words'."
        );
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
      <table class="table">
        <tbody>
          <tr v-for="file in words" :key="file.name">
            <td>{{ file.word }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>


<style scoped>
.clickable {
  cursor: pointer;
}
</style>