import { ref, computed } from "vue";
import { defineStore } from "pinia";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";
import Envelope from "@/models/Envelope";
import User from "@/models/User";
import GroupType from "@/constants/GroupType"

export const useChatStore = defineStore("chat", () => {
  let _hubConnection = null;
  let chatsData = ref({
    Global: [],
    Entertainment: [],
    VideoGames: [],
    Sports: []
  });

  function connectHub() {
    _hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7017/ChatHub',  {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();

    //Receive messages
    _hubConnection.on('GroupMessage', (envelope) => {
      console.log("incoming message")
      if (envelope.groupType == GroupType.All) {
        for (var property in chatsData.value) {
          chatsData.value[property].push(new Envelope(envelope.user, envelope.message, envelope.groupType, envelope.date));
        }
      }else{
        chatsData.value[envelope.groupType].push(new Envelope(envelope.user, envelope.message, envelope.groupType, envelope.date));
      }
    });

    return _hubConnection.start();
  }

  const getChatMessages = computed(() => chatsData.value);

  return { getChatMessages, connectHub, chatsData };
});
