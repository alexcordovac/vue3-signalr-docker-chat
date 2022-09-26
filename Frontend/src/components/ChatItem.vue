<template>
    <div class="chat-item my-2" :class="isSelected ? 'chat-item-selected' : ''" @click="raiseClickEvent">
        <div class="row h-100 mx-0 align-items-center ">
            <div class="col-3 chat-item_emoji text-center fs-4 px-0">
                {{emoji}}
            </div>
            <div class="col chat-item_text d-flex flex-column px-0">
                <div>
                    <h5>{{title}}</h5>
                    <p>{{description}}</p>

                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { defineComponent, ref } from "vue"

export default defineComponent({
    name: "ChatItem",
    emits: ["click"],
    props: {
        emoji: String,
        title: String,
        description: String,
        isSelected: Boolean,
        data: Array
    },
    setup(props, context){
        const messagesTemplate = ref(null);
        
        const getMessagesTemplate = () => {
            return messagesTemplate.value;
        }


        const raiseClickEvent = () =>{
            context.emit('click');
        }
        

        return{
            messagesTemplate,
            getMessagesTemplate,
            raiseClickEvent
        }
    }
});
</script>

<style scoped>
.container {
    width: 100%;
    height: 100px;
    border-radius: 25px;
    background-color: white;
}


.chat-item {
    background-color: #F7F7F7;
    border-radius: 5px;
    height: 60px;
    max-height: 60px;
}

.chat-item:hover {
    box-shadow: 0 1px 1px 0 rgba(0, 0, 0, 0.1), 0 2px 15px 0 rgba(0, 0, 0, 0.1);
    cursor: pointer;
}

.chat-item_text p,
.chat-item_text h5 {
    margin: 0;
}

.chat-item_text p {
    font-size: 0.7rem;
}

.chat-item-selected{
    color: white;
    background-color: #319167;
}
</style>