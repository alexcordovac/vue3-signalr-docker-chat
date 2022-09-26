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
        <div ref="messagesWindows" class="row chat-messages flex-nowrap flex-column flex-grow-1 align-items-start mx-2">
          <slot name="chatmessages"></slot>
        </div>
        

        <!-- send message -->
        <div class="row chat-writetext mb-3 justify-content-center">
          <form class="row" @submit.prevent="submitMessage">
            <div class="p-1 bg-light rounded rounded-pill shadow-sm mb-4">
              <div class="input-group">
                <input v-model="message" type="search" placeholder="Write message"
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
import { defineComponent, ref, nextTick  } from "vue"

export default defineComponent({
  name: "ChatLayout",
  props: {
    emoji: String,
    title: String,
    description: String
  },
  emits: ["submitMessage"],

  setup(props, context) {
    const message = ref('');
    const messagesWindows = ref(null);

    //Submit message
    const submitMessage = () => {
      context.emit('submitMessage', message.value);
      message.value = '';
    }

    const scrollMessages = async () => {
      //Wait mesages loadead in messages layout
      await nextTick();

      messagesWindows.value?.scrollIntoView(true);
      messagesWindows.value.scrollTop = messagesWindows.value.scrollHeight;
    }

    //expose 
    context.expose({ scrollMessages })

    return {
      submitMessage,
      message,
      messagesWindows
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