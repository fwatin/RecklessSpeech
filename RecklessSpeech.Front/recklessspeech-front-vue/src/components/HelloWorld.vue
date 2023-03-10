<template>
  <div>
    <v-btn @click="openFilePicker">Import items.csv</v-btn>
    <v-btn @click="openJsonPicker">Import lln_json_items_xxx.json</v-btn>
    <v-btn @click="enrichInDutch(selectedSequences)"
      >Enrichir en Néérlandais</v-btn
    >
    <v-card>
      <v-card-title>Liste de séquences</v-card-title>
      <v-card-text>
        <v-list>
          <v-list-item v-for="(sequence, index) in sequences" :key="index">
            <v-row>
              <v-col cols="6">
                <v-checkbox-btn
                  :model-value="checkedSequences[index]"
                ></v-checkbox-btn>
              </v-col>
              <v-col cols="6">
                <v-label>{{ sequence.word }}</v-label>
              </v-col>
            </v-row>
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
    <v-dialog v-model="filePickerDialog" max-width="500">
      <v-card>
        <v-card-title>Sélectionner un fichier CSV</v-card-title>
        <v-card-text>
          <input
            type="file"
            ref="fileInput"
            accept=".csv"
            @change="onFileSelected"
          />
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="importCSV">Importer</v-btn>
          <v-btn @click="filePickerDialog = false">Annuler</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="jsonPickerDialog" max-width="500">
      <v-card>
        <v-card-title>Sélectionner un fichier JSON</v-card-title>
        <v-card-text>
          <input
            type="file"
            ref="jsonInput"
            accept=".json"
            @change="onJsonSelected"
          />
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="importJSON">Importer</v-btn>
          <v-btn @click="jsonPickerDialog = false">Annuler</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import axios from "axios";
import { useToast } from "vue-toastification";

export default {
  setup() {
    const toast = useToast();
    return { toast };
  },
  data() {
    return {
      sequences: [],
      filePickerDialog: false,
      jsonPickerDialog: false,
      selectedFile: null,
      selectedJson: null,
      selectedSequences: [],
      checkedSequences: [],
    };
  },
  mounted() {
    axios
      .get("https://localhost:47973/api/v1/sequences")
      .then((response) => {
        this.sequences = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  methods: {
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
    async importCSV() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedFile);

        const response = await axios.post(
          "https://localhost:47973/api/v1/sequences",
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );

        console.log(response.data);
        // Mettre à jour la liste des séquences si besoin

        this.toast.info("Le fichier CSV a été importé avec succès.");
      } catch (error) {
        console.error(error);
        this.toast.info(
          "Une erreur est survenue lors de l'importation du fichier CSV."
        );
      }

      this.filePickerDialog = false;
    },
    async importJSON() {
      try {
        const formData = new FormData();
        formData.append("file", this.selectedJson);

        const response = await axios.post(
          "https://localhost:47973/api/v1/sequences/import-details",
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );

        console.log(response.data);
        // Mettre à jour la liste des séquences si besoin

        this.toast.info("Le fichier Json a été importé avec succès.");
      } catch (error) {
        console.error(error);
        this.toast.info(
          "Une erreur est survenue lors de l'importation du fichier Json."
        );
      }

      this.jsonPickerDialog = false;
    },
    async enrichInDutch(selectedSequences) {
      try {
        for (const sequence of selectedSequences) {
          const response = await axios.post(
            `https://localhost:47973/api/v1/sequences/Dictionary/dutch?id=${sequence.id}`
          );

          console.log(response.data);
          // Mettre à jour la liste des séquences si besoin
        }

        this.toast.info("Les séquences ont été enrichies avec succès.");
      } catch (error) {
        console.error(error);
        this.toast.info(
          "Une erreur est survenue lors de l'enrichissement des séquences."
        );
      }
    },
  },
};
</script>