<template>
  <ChatLayout>
    <template v-slot:chatitems>
      <div v-for="chat in availableChats">
        <ChatItem @click="changeChat(chat)" :title="chat.title" :emoji="chat.emoji" :description="chat.description" />
      </div>
    </template>


    <template v-slot:chatmessages>
      <template v-for="chat in availableChats">
        <template v-if="chat.title == selectedChat">
          <div v-for="envelope in chat.data" class="message-wrapper text-white message-left ps-3 pb-2">
            <p class="text-primary text-left m-0 p-0 mb-2">{{envelope.user}}</p>
            <p class="p-0 m-0 message-wrapper_text">{{envelope.message}}</p>
          </div>
        </template>
      </template>
    </template>

  </ChatLayout>
</template>


<script>
import Swal from 'sweetalert2'
import { defineComponent, onMounted, ref } from "vue"
import { useChatStore } from "@/stores/useChatStore"
import { useUserStore } from "@/stores/useUserStore"
import ChatLayout from '@/layouts/ChatLayout.vue';
import ChatItem from '@/components/ChatItem.vue';

export default defineComponent({
  name: "ChatPage",
  components: {
    ChatLayout,
    ChatItem
  },

  setup() {
    const chatStore = useChatStore();
    const userStore = useUserStore();

    const availableChats = [
      { title: 'Global', emoji: '🌎', description: 'Make friends all around the world!', data: chatStore.chatsData["Global"] },
      { title: 'Entertainment', emoji: '✌', description: '123', data: chatStore.chatsData["Entertainment"] },
      { title: 'Video Games', emoji: '🎮', description: '456', data: chatStore.chatsData["VideoGames"] }

    ]
    let selectedChat = ref('');

    const changeChat = (c) => {
      console.log(c.title);
      selectedChat.value = c.title;
    };


    //Connect to Chat Hub 
    onMounted(async () => {

      try {
        await chatStore.connectHub();
        console.log('Conectado correctamente');



      } catch (error) {
        console.log('Something went wrong.');
      }


      const { value: userinput } = await Swal.fire({
        title: 'Enter your username',
        input: 'text',
        inputLabel: 'Username',
        inputValue: '',
        showCancelButton: true,
        inputValidator: (value) => {
          if (!value) {
            return 'You need to write something!'
          }
        }
      })

      userStore.user.username = userinput;
    });


    return {
      chatStore,
      changeChat,
      availableChats,
      selectedChat
    };
  },
});
</script>

<style scoped>
.message-wrapper {
  min-height: 30px;
  min-width: 100px;
  max-width: 70%;
  height: fit-content;
  width: fit-content;
  margin: 5px;
}


.message-wrapper.message-left {
  background-color: #838282;
  border-radius: 20px 20px 20px 0;
}

.message-wrapper.message-right {
  background-color: #319167;
  border-radius: 20px 20px 0px 20px;
}


.message-wrapper_text {
  line-height: 20px;
}
</style>