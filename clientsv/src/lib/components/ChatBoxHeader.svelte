<script>
	import { chatsStore } from '$lib/global.js';
	import { selectedChatId } from '$lib/global.js';
	import { userId } from '$lib/global.js';
	import { fade } from 'svelte/transition';
	import { Avatar } from 'flowbite-svelte';
	import { CogOutline, PhoneOutline, VideoCameraOutline } from 'flowbite-svelte-icons';
</script>

<div class="min-h-16 bg-[#202938] w-full flex items-center pl-5 pr-20 border-b-1 border-grayBorder">
	{#key $selectedChatId}
	<div in:fade class="flex justify-between w-full">
		<div class="text-white flex items-center gap-5">
			<div>
				<Avatar src="https://media.istockphoto.com/id/1364917563/photo/businessman-smiling-with-arms-crossed-on-white-background.jpg?s=612x612&w=0&k=20&c=NtM9Wbs1DBiGaiowsxJY6wNCnLf0POa65rYEwnZymrM="/>
			</div>
			<div>
				{$selectedChatId}
			</div>
		</div>

		<div class="text-white flex justify-between items-center gap-5">
			<div class="flex items-center justify-center rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200 p-2">
				<VideoCameraOutline class="w-8 h-8 text-green-400"/>
			</div>

			<div class="flex items-center justify-center rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200 p-2">
				<PhoneOutline class="w-8 h-8 text-green-400"/>
			</div>

		</div>
	</div>
	{/key}
</div>