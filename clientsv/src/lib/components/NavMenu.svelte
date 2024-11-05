<script>
	import { Search, Button } from 'flowbite-svelte';
	import { MicrophoneSolid, SearchOutline } from 'flowbite-svelte-icons';
	import { writable } from 'svelte/store';

	export let connection; // SignalR connection instance

	let userId = ''; // Stores user ID for starting a chat
	let chatStatus = writable(''); // Status message for feedback

	async function createChat() {
		if (!userId) {
			chatStatus.set('Please enter a valid User ID.');
			return;
		}

		try {
			// Invoke the `CreateChat` method on the SignalR hub
			await connection.invoke('CreateChat', userId);

			// Update status to show that the chat was created
			chatStatus.set(`Chat created with User ID: ${userId}`);
		} catch (error) {
			console.error('Error creating chat:', error);
			chatStatus.set('Failed to create chat. Try again.');
		}
	}

	function handleVoiceBtn() {
		alert('You clicked the voice button');
	}
</script>

<div class="min-h-16 bg-[#202938] w-full flex items-center pl-5 pr-5 border-b-1 border-grayBorder">
	<div class="min-w-full">
		<form class="flex gap-2">
			<Search bind:value={userId} size="" class="flex gap-2 items-center" placeholder="Search Contacts...">
				<button type="button" on:click={handleVoiceBtn} class="outline-none">
					<MicrophoneSolid class="w-5 h-5 me-2" />
				</button>
			</Search>
			<Button on:click={createChat} class="!p-2.5">
				<SearchOutline class="w-6 h-6" />
			</Button>
		</form>
	</div>
</div>
