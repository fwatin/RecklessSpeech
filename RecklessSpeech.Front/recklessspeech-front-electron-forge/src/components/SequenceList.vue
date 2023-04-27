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
      isEnriching: false,
      isSendingToAnki: false,
      sendToAnkiProgression: 0,
    };
  },
  methods: {
    async enrichInDutch() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      this.enrichProgression = 0;
      this.isEnriching = true;
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
      this.isEnriching = false;
      let msg =
        selectedWords.length +
        " séquences ont été enrichies avec succès en néérlandais.";
      console.log(msg);
      new Notification(msg);
    },
    async enrichInEnglish() {
      const selectedWords = this.words.filter((word, index) => {
        return this.checkedWords[index];
      });
      this.enrichProgression = 0;
      this.isEnriching = true;
      let enrichCount = 0;
      let total = selectedWords.length;
      for (const sequence of selectedWords) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/Dictionary/english?id=${id}`
        );
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isEnriching = false;

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
      this.sendToAnkiProgression = 0;
      let sendToAnkiCount = 0;
      let total = selectedWords.length;
      this.isSendingToAnki = true;
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
        sendToAnkiCount++;
        this.sendToAnkiProgression = Math.round(
          (sendToAnkiCount * 100) / total
        );
      }
      this.isSendingToAnki = false;
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

        const response = await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/import-details`,
            formData,
            {
              headers: {
                "Content-Type": "multipart/form-data",
              },
            }
          )
          .then((response) => {
            this.words = response.data;
          });
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
  <div class="container mt-5">

    <!-- Importer un fichier Zip -->
    <div class="card mb-4">
      <div class="card-header">Sélectionner un fichier Zip</div>
      <div class="card-body">
        <div class="input-group">
          <input type="file" class="form-control" ref="fileInput" accept=".zip" @change="onFileSelected" />
          <button class="btn btn-primary ml-3" @click="importZip">Importer</button>
        </div>
      </div>
    </div>

    <!-- Importer un fichier JSON -->
    <div class="card mb-4">
      <div class="card-header">Sélectionner un fichier JSON</div>
      <div class="card-body">
        <div class="input-group">
          <input type="file" class="form-control" ref="fileInput" accept=".json" @change="onJsonSelected" />
          <button class="btn btn-primary ml-3" @click="importJSON">Importer</button>
        </div>
      </div>
    </div>

    <!-- Enrichir -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <div v-if="isEnriching" class="spinner-border text-primary mr-2" role="status"></div>
          Enrichir {{ this.enrichProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-secondary mr-2" @click="selectAll()">Sélectionner tout</button>
        <button class="btn btn-info mr-2" @click="enrichInEnglish()">Enrichir en anglais</button>
        <button class="btn btn-info" @click="enrichInDutch()">Enrichir en néérlandais</button>
      </div>
    </div>

    <!-- Envoyer à Anki -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <div v-if="isSendingToAnki" class="spinner-border text-primary mr-2" role="status"></div>
          Envoyer {{ this.sendToAnkiProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-primary" @click="sendToAnki()">Envoyer vers Anki</button>
      </div>
    </div>

    <!-- Tableau des mots -->
    <div class="card">
      <div class="card-body">
        <table class="table">
          <tbody>
            <tr v-for="(file, index) in words" :key="file.name">
              <td>
                <input type="checkbox" class="form-check-input" v-model="checkedWords[index]" />
                <span>{{ file.word }}</span>
              </td>
              <td>
                <span>{{ file.translatedWord }}</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

  </div>
</template>

<style scoped>
/* Pas besoin de styles spécifiques ici car nous utilisons Bootstrap */
</style>
