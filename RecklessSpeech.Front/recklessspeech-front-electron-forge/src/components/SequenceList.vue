<script>
import axios from "axios";
const backendPort = process.env.NODE_ENV === "development" ? "47973" : "5001";
export default {
  data() {
    return {
      filePickerDialog: false,
      jsonPickerDialog: false,
      words: [],
      checkedWords: [],
      selectedFile: null,
      selectedJson: null,
      enrichProgression: 0,
    };
  },
  methods: {
    async enrichInDutch() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      this.enrichProgression = 0;
      let enrichCount = 0;
      let total = selectedWords.length;
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/Dictionary/dutch?id=${id}`
        );
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      let msg =
        selectedWords.length +
        " séquences ont été enrichies avec succès en néérlandais.";
      console.log(msg);
      new Notification(msg);
    },
    async enrichInEnglish() {
      this.enrichProgression = 0;
      let enrichCount = 0;
      let total = selectedWords.length;
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/Dictionary/english?id=${id}`
        );
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }

      let msg =
        selectedWords.length +
        " séquences ont été enrichies avec succès en anglais.";
      console.log(msg);
      new Notification(msg);
    },
    async sendToAnki() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/send-to-anki?id=${id}`
          )
          .then(() => {
            new Notification(`${sequence.word} successfully sent to Anki.`);
          })
          .catch(() => {
            new Notification(`${sequence.word} failed to be sent to Anki.`);
          });
      }
    },
    openFilePicker() {
      this.filePickerDialog = true;
    },
    openJsonPicker() {
      this.jsonPickerDialog = true;
    },
    onFileSelected(event) {
      this.selectedFile = event.target.files[0];
    },
    onJsonSelected(event) {
      this.selectedJson = event.target.files[0];
    },
    selectAll() {
      if (this.checkedWords.some((isChecked) => isChecked)) {
        // Au moins un élément est sélectionné, donc on les désélectionne tous
        this.checkedWords = [];
      } else {
        // Aucun élément n'est sélectionné, donc on les sélectionne tous
        this.checkedWords = this.words.map(() => true);
      }
    },
    async importZip() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedFile);

        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/import-zip`,
            formData,
            {
              headers: {
                "Content-Type": "multipart/form-data",
              },
            }
          )
          .then((response) => {
            this.words = response.data;
            console.log(
              this.words.length + " words set into the variable 'words'."
            );
            new Notification(this.words.length + " mots ont été importés.");
          });
      } catch (error) {
        console.error(error);
        new Notification(
          "Une erreur est survenue lors de l'importation du fichier zip."
        );
      }

      this.filePickerDialog = false;
    },
    async importJSON() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedJson);

        const response = await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/import-details`,
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );
        console.log(
          "import-details http call ended with status: " + response.status
        );
        let msg = "Importation du fichier Json avec succès.";
        console.log(msg);
        new Notification(msg);
      } catch (error) {
        console.error(error);

        let msg =
          "Une erreur est survenue lors de l'importation du fichier Json.";
        console.log(msg);
        new Notification(msg);
      }

      this.jsonPickerDialog = false;
    },
  },
};
</script>
<template>
  <div>
    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Sélectionner un fichier Zip</legend>
        <p>
          <input
            type="file"
            ref="fileInput"
            accept=".zip"
            @change="onFileSelected"
          />
          <button @click="importZip">Importer</button>
        </p>
      </fieldset>
    </div>

    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Sélectionner un fichier JSON</legend>
        <p>
          <input
            type="file"
            ref="fileInput"
            accept=".json"
            @change="onJsonSelected"
          />
          <button @click="importJSON">Importer</button>
        </p>
      </fieldset>
    </div>

    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">
          Enrichir {{ this.enrichProgression }}%
        </legend>
        <button class="clickable button-margin" @click="selectAll()">
          Selectionner tout
        </button>
        <button class="clickable button-margin" @click="enrichInEnglish()">
          Enrichir en anglais
        </button>
        <button class="clickable button-margin" @click="enrichInDutch()">
          Enrichir en néérlandais
        </button>
      </fieldset>
    </div>
    <div class="fieldset-container">
      <fieldset style="border: 2px solid #000; padding: 10px">
        <legend style="font-size: 20px">Envoyer</legend>
        <button class="clickable button-margin" @click="sendToAnki()">
          Envoyer vers Anki
        </button>
      </fieldset>
    </div>

    <div>
      <table class="table">
        <tbody>
          <tr v-for="(file, index) in words" :key="file.name">
            <td>
              <input
                type="checkbox"
                class="checkboxes"
                v-model="checkedWords[index]"
              />
              <span>{{ file.word }}</span>
            </td>
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
.button-margin {
  margin: 5px;
}
.fieldset-container {
  margin: 5px;
}
.checkboxes {
  margin: 5px;
}
</style>