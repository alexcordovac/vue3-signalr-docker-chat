<template>
  <div class="container-fluid chat-container vh-75 vw-75">

    <div class="row h-100">

      <!-- left column  -->
      <div class="col-3 chat-container_left mh-100">

        <!-- title -->
        <div class="px-3 mt-3 py-2">
          <h3>Chats</h3>
        </div>

        <!-- chat items container -->
        <div class="w-100 mt-4">
          <slot name="chatitems"></slot>
        </div>

      </div>

      <!-- right column  -->
      <div class="col chat-container_right d-flex flex-column mh-100">
        <div class="row chat-info align-items-center text-white">
          <div class="col-auto text-center fs-4">
            <slot name="chatemoji"></slot>
          </div>
          <div class="col text-left ps-0">
            <h5 class="m-0">
              <slot name="chattitle"></slot>
            </h5>
          </div>

        </div>


        <!-- messages -->
        <div class="row chat-messages flex-nowrap flex-column flex-grow-1 align-items-start mx-2">

          <slot name="chatmessages"></slot>
          
          <!-- <div class="row align-items-center text-white">
            <div class="message-wrapper message-left ps-3 pb-2">
              <p class="text-primary text-left m-0 p-0 mb-2">alexcordovac</p>
              <p class="p-0 m-0 message-wrapper_text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Nesciunt
                ullam ex aperiam delectus. Minima aliquam placeat numquam odit voluptatum impedit esse. Praesentium
                reprehenderit adipisci et, nobis officiis minus at quos.</p>
            </div>
          </div>

          <div class="row align-items-center justify-content-end text-white">
            <div class="message-wrapper message-right ps-3 pb-2">
              <p class="text-primary text-left m-0 p-0 mb-2">alexcordovac</p>
              <p class="p-0 m-0 message-wrapper_text">Lorem ipsum dolor sit </p>
            </div>
          </div> -->

        </div>
        

        <!-- send message -->
        <div class="row chat-writetext mb-3 justify-content-center">
          <form class="row" @submit.prevent="submitMessage">
            <div class="p-1 bg-light rounded rounded-pill shadow-sm mb-4">
              <div class="input-group">
                <input v-model="message.message" type="search" placeholder="Write message"
                  aria-describedby="button-addon1" class="form-control border-0 bg-light rounded rounded-pill">
                <div class="input-group-append">
                  <button id="button-addon1" type="submit" class="btn btn-link text-primary">
                    <i class="bi bi-send-fill"></i>
                  </button>
                </div>
              </div>
            </div>
          </form>
        </div>

      </div>

    </div>
  </div>
</template>

<script>
import { defineComponent } from "vue"
import Envelope from "@/models/Envelope"
import { useUserStore } from "@/stores/useUserStore"

export default defineComponent({
  name: "ChatLayout",
  props: {
    emoji: String,
    title: String,
    description: String
  },

  setup() {
    const userStore = useUserStore();
    const message = new Envelope(userStore.user.username, '');

    //Submit message
    const submitMessage = () => {
      console.log(message);
    }

    return {
      submitMessage,
      message
    }
  }


});
</script>

<style scoped>
.chat-container {}

.chat-container_left {
  background-color: #EDEDED;
  border-radius: 15px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.1), 0 4px 8px 0 rgba(0, 0, 0, 0.1);
  margin-right: 15px;
}


.chat-container_right {
  background-color: #EDEDED;
  border-radius: 15px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.1), 0 4px 8px 0 rgba(0, 0, 0, 0.1);
}


.chat-info {
  height: 50px;
  background-color: #319167;
  border-radius: 15px 15px 0 0;
}

.chat-writetext {
  height: 50px;
}

.form-control:focus {
  box-shadow: none;
}

.form-control-underlined {
  border-width: 0;
  border-bottom-width: 1px;
  border-radius: 0;
  padding-left: 0;
}

.chat-messages {
  flex-basis: 0;
  overflow-y: auto;
}

</style>