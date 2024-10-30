<script>
	import { chatsStore } from '$lib/global.js';
	import { fade } from 'svelte/transition';
	import { selectedChatId } from '$lib/global.js';
	import ChatBoxHeader from '$lib/components/ChatBoxHeader.svelte';
	import { afterUpdate } from 'svelte';
	import { Button, Input, Label } from 'flowbite-svelte';
	import Message from '$lib/components/Message.svelte'; // Import your Message component

	export let connection;
	let message = '';
	let messagesContainer;

	function sendMessage() {
		if (!$selectedChatId || !message) return;

		connection.invoke('SendMsg', $selectedChatId, message)
			.then(() => {
				$chatsStore.update(chatData => {
					if (!chatData[$selectedChatId]) {
						chatData[$selectedChatId] = { messages: [] };
					}
					chatData[$selectedChatId].messages.push({ user: 'You', message });
					return chatData;
				});
				message = '';
			})
			.catch(err => console.error('Error sending message:', err));
	}

	afterUpdate(() => {
		if (messagesContainer) {
			messagesContainer.scrollTop = messagesContainer.scrollHeight;
		}
	});
</script>

<div class="flex flex-col flex-1 w-full bg-[#111828]">
	<ChatBoxHeader />
	<div
		bind:this={messagesContainer}
		class="flex-1 overflow-y-auto p-5 pr-10"
	>
		{#key $selectedChatId}
			<div in:fade>
				{#if $selectedChatId}
					{#each $chatsStore[$selectedChatId]?.messages as msg}
						<Message
							user={msg.user}
							message={msg.message}
						/>
					{/each}
				{:else}
					<div class="text-gray-400">No chat selected.</div>
				{/if}
			</div>
		{/key}
	</div>

	<div class="flex items-center p-2 border-t-1 border-grayBorder">
		<div class="min-w-full">
			<form on:submit|preventDefault={sendMessage}>
				<Input bind:value={message} id="search" placeholder="Type a message...">
					<Button slot="right" size="sm" type="submit">Send</Button>
				</Input>
			</form>
		</div>
	</div>
</div>
