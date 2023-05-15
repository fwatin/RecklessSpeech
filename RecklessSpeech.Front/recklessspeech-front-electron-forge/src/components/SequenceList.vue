<script>
import axios from "axios";
const backendPort = process.env.NODE_ENV === "development" ? "47973" : "5001";
export default {
  data() {
    return {
      filePickerDialog: false,
      jsonPickerDialog: false,
      sequences: [],
      checkedSequences: [],
      selectedFile: null,
      selectedJson: null,
      enrichProgression: 0,
      isEnriching: false,
      isSendingToAnki: false,
      sendToAnkiProgression: 0,
      lastSelectedSequenceIndex: null,
    };
  },
  methods: {
    selectSequence(index, isShiftPressed) {
      if (isShiftPressed && this.lastSelectedSequenceIndex !== null) {
        const start = Math.min(this.lastSelectedSequenceIndex, index);
        const end = Math.max(this.lastSelectedSequenceIndex, index);
        this.checkedSequences = [];
        for (let i = start; i <= end; i++) {
          this.checkedSequences.push(i);
        }
      } else {
        this.lastSelectedSequenceIndex = index;
        const isChecked = this.checkedSequences.includes(index);
        if (isChecked) {
          this.checkedSequences = this.checkedSequences.filter(
            (i) => i !== index
          );
        } else {
          this.checkedSequences.push(index);
        }
      }
    },

    toggleCheckedSequence(index) {
      const isChecked = this.checkedSequences.includes(index);
      if (isChecked) {
        this.checkedSequences = this.checkedSequences.filter(
          (i) => i !== index
        );
      } else {
        this.checkedSequences.push(index);
      }
    },
    selectAll() {
      if (this.checkedSequences.length === this.sequences.length) {
        this.checkedSequences = [];
      } else {
        this.checkedSequences = this.sequences.map((_, index) => index);
      }
    },
    async enrichInDutch() {
      this.enrichProgression = 0;
      this.isEnriching = true;
      let enrichCount = 0;
      const selectedIndices = this.checkedSequences
        .map((isChecked, index) => (isChecked ? index : null))
        .filter((index) => index !== null);
      const total = selectedIndices.length;
      for (const index of selectedIndices) {
        const sequence = this.sequences[index];
        const id = sequence.id;
        await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/Dictionary/dutch?id=${id}`
        );
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isEnriching = false;
      const msg =
        selectedIndices.length +
        " séquences ont été enrichies avec succès en néerlandais.";
      console.log(msg);
      new Notification(msg);
    },
    async enrichInEnglish() {
      const selectedSequences = this.sequences.filter((index) => {
        return this.checkedSequences[index];
      });
      this.enrichProgression = 0;
      this.isEnriching = true;
      let enrichCount = 0;
      let total = selectedSequences.length;
      for (const sequence of selectedSequences) {
        let id = sequence.id;
        await axios.post(
          `https://localhost:${backendPort}/api/v1/sequences/Dictionary/english?id=${id}`
        );
        enrichCount++;
        this.enrichProgression = Math.round((enrichCount * 100) / total);
      }
      this.isEnriching = false;

      let msg =
        selectedSequences.length +
        " séquences ont été enrichies avec succès en anglais.";
      console.log(msg);
      new Notification(msg);
    },
    async sendToAnki() {
      const selectedSequences = this.sequences.filter((word, index) => {
        return this.checkedSequences[index];
      });
      this.sendToAnkiProgression = 0;
      let sendToAnkiCount = 0;
      let total = selectedSequences.length;
      this.isSendingToAnki = true;
      for (const sequence of selectedSequences) {
        let id = sequence.id;
        await axios
          .post(
            `https://localhost:${backendPort}/api/v1/sequences/send-to-anki?id=${id}`
          )
          .catch(() => {
            new Notification(`${sequence.word} failed to be sent to Anki.`);
          });
        sendToAnkiCount++;
        this.sendToAnkiProgression = Math.round(
          (sendToAnkiCount * 100) / total
        );
      }
      this.isSendingToAnki = false;
      new Notification(`${selectedSequences.length} sequences sent to Anki.`);
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
            this.sequences = response.data;
            console.log(
              this.sequences.length + " words set into the variable 'words'."
            );
            new Notification(this.sequences.length + " mots ont été importés.");
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
            this.sequences = response.data;

            let msg = "Importation du fichier Json avec succès.";
            console.log(msg);
            new Notification(msg);
          });
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
          <input
            type="file"
            class="form-control"
            ref="fileInput"
            accept=".zip"
            @change="onFileSelected"
          />
          <button class="btn btn-primary ml-3" @click="importZip">
            Importer
          </button>
        </div>
      </div>
    </div>

    <!-- Importer un fichier JSON -->
    <div class="card mb-4">
      <div class="card-header">Sélectionner un fichier JSON</div>
      <div class="card-body">
        <div class="input-group">
          <input
            type="file"
            class="form-control"
            ref="fileInput"
            accept=".json"
            @change="onJsonSelected"
          />
          <button class="btn btn-primary ml-3" @click="importJSON">
            Importer
          </button>
        </div>
      </div>
    </div>

    <!-- Enrichir -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <div
            v-if="isEnriching"
            class="spinner-border text-primary mr-2"
            role="status"
          ></div>
          Enrichir {{ this.enrichProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-secondary mr-2" @click="selectAll()">
          Sélectionner tout
        </button>
        <button class="btn btn-info mr-2" @click="enrichInEnglish()">
          Enrichir en anglais
        </button>
        <button class="btn btn-info" @click="enrichInDutch()">
          Enrichir en néérlandais
        </button>
      </div>
    </div>

    <!-- Envoyer à Anki -->
    <div class="card mb-4">
      <div class="card-header">
        <div class="d-flex align-items-center">
          <div
            v-if="isSendingToAnki"
            class="spinner-border text-primary mr-2"
            role="status"
          ></div>
          Envoyer {{ this.sendToAnkiProgression }}%
        </div>
      </div>
      <div class="card-body">
        <button class="btn btn-primary" @click="sendToAnki()">
          Envoyer vers Anki
        </button>
      </div>
    </div>

    <!-- Liste des mots -->
    <div class="card">
      <div class="card-body">
        <div class="list-group">
          <div
            v-for="(sequence, index) in sequences"
            :key="sequence.id"
            :class="[
              'list-group-item',
              { active: checkedSequences.includes(index) },
            ]"
            @mousedown="selectSequence(index, $event.shiftKey)"
            @keydown.space.prevent="toggleCheckedSequence(index)"
            tabindex="0"
          >
            <input
              type="checkbox"
              class="form-check-input"
              v-model="checkedSequences"
              :value="index"
            />
            <span>{{ sequence.word }}</span>
            <span class="ml-2">{{ sequence.translatedWord }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>