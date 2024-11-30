<script>
	import { chatsStore } from '$lib/global.js';
	import NavMenu from '$lib/components/NavMenu.svelte';
	import { selectedChatId } from '$lib/global.js';
	import { Avatar } from 'flowbite-svelte';
	export let connection;
</script>
<div class="flex-col bg-[#111828] border-r-1 border-grayBorder">
	<NavMenu {connection} />

	<div class="flex-col overflow-y-auto bg-[#111828]">
			{#each Object.entries($chatsStore) as [chatId, chat] (chatId)}
				<div role="button" tabindex="0" on:keydown={() => console.log("KD")} class="flex gap-3 items-center text-white m-3 bg-[#202938] hover:bg-[#39414f] p-4 rounded hover:cursor-pointer" on:click={() => $selectedChatId = chatId}>
					<div>
						<Avatar src="https://media.istockphoto.com/id/1364917563/photo/businessman-smiling-with-arms-crossed-on-white-background.jpg?s=612x612&w=0&k=20&c=NtM9Wbs1DBiGaiowsxJY6wNCnLf0POa65rYEwnZymrM="/>
					</div>
					<div>
						{chatId}
					</div>
				</div>
			{/each}
	</div>
</div>

