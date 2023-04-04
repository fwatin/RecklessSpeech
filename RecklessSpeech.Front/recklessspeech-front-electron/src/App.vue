<script>
import fs from "fs";
import pathModule from "path";
import { computed, ref } from "vue";
import { app } from "@electron/remote";
import { file } from "@babel/types";

const formatSize = (size) => {
  var i = Math.floor(Math.log(size) / Math.log(1024));
  return (
    (size / Math.pow(1024, i)).toFixed(2) * 1 +
    " " +
    ["B", "kB", "MB", "GB", "TB"][i]
  );
};

export default {
  setup() {
    const path = ref(app.getAppPath());
    const files = computed(() => {
      const fileNames = fs.readdirSync(path.value);
      return fileNames
        .map((file) => {
          const stats = fs.statSync(pathModule.join(path.value, file));
          return {
            name: file,
            size: stats.isFile() ? formatSize(stats.size ?? 0) : null,
            directory: stats.isDirectory(),
          };
        })
        .sort((a, b) => {
          if (a.directory == b.directory) {
            return a.name.localeCompare(b.name);
          }
          return a.directory ? -1 : 1;
        });
    });

    const back = () => {
      path.value = pathModule.dirname(path.value);
    };

    const open = (folder) => {
      path.value = pathModule.join(path.value, folder);
    };
    const searchString = ref("");
    const filteredFiles = computed(() => {
      return searchString.value
        ? files.value.filter((s) => s.name.startsWith(searchString.value))
        : files.value;
    });

    return {
      path,
      open,
      back,
      files,
      searchString,
      filteredFiles,
    };
  },
};
</script>

<template>
  <div class="container mt-2">
    <div class="form-group mt-4 mb-2">
      <input class="form-control form-control-sm" placeholder="File search" />
    </div>
  </div>
</template>
