<script>
	import { Search, Button } from 'flowbite-svelte';
	import { MicrophoneSolid, SearchOutline } from 'flowbite-svelte-icons';
	function handleVoiceBtn() {
		alert('You clicked voice button');
	}
</script>

<div class="min-h-16 bg-[#202938] w-full flex items-center pl-5 pr-5 border-b-1 border-grayBorder">
	<div class="min-w-full">
		<form class="flex gap-2">
			<Search size="" class="flex gap-2 items-center" placeholder="Search Contacts...">
				<button type="button" on:click={handleVoiceBtn} class="outline-none">
					<MicrophoneSolid class="w-5 h-5 me-2" />
				</button>
			</Search>
			<Button class="!p-2.5">
				<SearchOutline class="w-6 h-6" />
			</Button>
		</form>
	</div>
</div>
