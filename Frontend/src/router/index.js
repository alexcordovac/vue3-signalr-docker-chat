import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      redirect: "/chat"
    },
    {
      path: "/chat",
      name: "Chat",
      component: () => import("@/pages/ChatPage.vue"),
    }
  ],
});

export default router;
