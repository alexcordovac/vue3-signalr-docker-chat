import { ref, computed } from "vue";
import { defineStore } from "pinia";
import { HubConnectionBuilder } from "@microsoft/signalr";
import Envelope from "@/models/Envelope"

export const useChatStore = defineStore("chat", () => {
  let _hubConnection = null;
  let chatsData = ref({
    Global: [new Envelope("Alex", "Hola"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q") , new Envelope("Alex", "Lorem12341 qwe1 1 q") ,new Envelope("Alex", "Lorem12341 qwe1 1 q") , new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q"), new Envelope("Alex", "Lorem12341 qwe1 1 q") ],
    Entertainment: [new Envelope("Alex", "Nuevo mensaje Entertainment") ],
    VideoGames: [new Envelope("Alex", "Nuevo mensaje VideoGames") ],
    Sports: [new Envelope("Alex", "Nuevo mensaje Sports") ]
  });

  function connectHub() {
    _hubConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:7017/ChatHub')
    .build();

    //Receive messages
    _hubConnection.on('ReceiveMessages', (groupname, message) => {
      chatsData.value[groupname].push(new Envelope(message.username, message.content, message.date));
    });

    return _hubConnection.start();
  }

  const getChatMessages = computed(() => chatsData.value);

  return { getChatMessages, connectHub, chatsData};
});
