<template>
  <ChatLayout ref="chatLayout" @submitMessage="submitMessage">

    <!-- Chat items -->
    <template v-slot:chatitems>
      <div v-for="chat in availableChats">
        <ChatItem @click="changeChat(chat)" :title="chat.title" :emoji="chat.emoji" :description="chat.description"
          :isSelected="chat.isSelected" />
      </div>
    </template>

    <!-- Chat emoji -->
    <template v-slot:chatemoji>
      {{selectedChat?.emoji}}
    </template>

    <!-- Chat Title -->
    <template v-slot:chattitle>
      {{selectedChat?.title}}
    </template>

    <!-- Chat messages -->
    <template v-slot:chatmessages>
      <template v-for="chat in availableChats">
        <template v-if="chat.title == selectedChat?.title">
          <div v-for="envelope in chat.data" class="message-wrapper text-white ps-3 pb-2"
            :class="userStore.user.username == envelope.user.username ? 'message-right' : 'message-left'">
            <p class="text-primary text-left m-0 p-0 mb-2">{{envelope.user.username}}</p>
            <p class="p-0 m-0 message-wrapper_text">{{envelope.message}}</p>
          </div>
        </template>
      </template>
    </template>

  </ChatLayout>
</template>


<script>
import moment from 'moment'
import Swal from 'sweetalert2'
import { defineComponent, onMounted, ref } from "vue"
import { useChatStore } from "@/stores/useChatStore"
import { useUserStore } from "@/stores/useUserStore"
import ChatLayout from '@/layouts/ChatLayout.vue'
import ChatItem from '@/components/ChatItem.vue'
import GroupType from "@/constants/GroupType"
import Envelope from '../models/Envelope'

export default defineComponent({
  name: "ChatPage",
  components: {
    ChatLayout,
    ChatItem
  },

  setup() {
    const chatStore = useChatStore();
    const userStore = useUserStore();
    const chatLayout = ref(null);

    const availableChats = [
      { title: 'Global', group: GroupType.Global, emoji: '🌎', description: 'Make friends all around the world!', isSelected: false, data: chatStore.chatsData[GroupType.Global] },
      { title: 'Entertainment', group: GroupType.Entertainment, emoji: '✌', description: '123', isSelected: false, data: chatStore.chatsData[GroupType.Entertainment] },
      { title: 'Video Games', group: GroupType.VideoGames, emoji: '🎮', description: '456', isSelected: false, data: chatStore.chatsData[GroupType.VideoGames] }

    ]
    let selectedChat = ref(null);

    const changeChat = (c) => {
      //Unselect last room
      if (selectedChat.value)
        selectedChat.value.isSelected = !selectedChat.value.isSelected;

      selectedChat.value = c;
      c.isSelected = !c.isSelected;

      //Scroll to bottom (newest messages)
      chatLayout.value.scrollMessages();
    };

    //Submit message to group
    const submitMessage = (message) => {
      if (selectedChat.value) {

        fetch('https://localhost:7017/api/Chat/Send', {
          method: 'POST',
          headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(new Envelope(userStore.user, message, selectedChat.value.group, moment().format('YYYY-MM-DDTHH:mm:ss[Z]')))
        })
        .then(res => {
          console.log(res);
          return res.text()
        })
        .then(res => {
          console.log(res);
        })
        .catch((error) => {
          console.log('Error:', error);
        });
      }
    }


    //Connect to Chat Hub 
    onMounted(async () => {

      const { value: userinput } = await Swal.fire({
        title: 'Enter your username',
        input: 'text',
        inputLabel: 'Username',
        inputValue: '',
        showCancelButton: false,
        allowOutsideClick: false,
        inputValidator: (value) => {
          if (!value) {
            return 'You need to write something!'
          }
        }
      })

      userStore.user.username = userinput;



      //Connect server
      try {
        await chatStore.connectHub();
        console.log('Conectado correctamente');
      } catch (error) {
        console.log('Something went wrong.', error);
      }

    });


    return {
      chatStore,
      userStore,
      changeChat,
      availableChats,
      selectedChat,
      chatLayout,
      submitMessage
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
  align-self: flex-start;
}

.message-wrapper.message-right {
  background-color: #319167;
  border-radius: 20px 20px 0px 20px;
  align-self: flex-end;
}


.message-wrapper_text {
  line-height: 20px;
}
</style>