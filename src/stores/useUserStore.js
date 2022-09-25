import { ref, computed } from "vue";
import { defineStore } from "pinia";
import User from "@/models/User"

export const useUserStore = defineStore("userprofile", () => {
  let user = ref(new User());

 
  return { user};
});
