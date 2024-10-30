<script>
	export let user;
	export let message;
	import { userId } from '$lib/global.js';
	import { fade } from 'svelte/transition';
</script>

<div class="flex flex-col w-full m-2">
	<strong class={`font-semibold text-white ${user === $userId ? 'text-right ml-auto' : 'text-left mr-auto'}`}>
		Name: {user}
	</strong>

	<div in:fade
			 class={`inline-block text-sm min-w-[100px] max-w-[400px] p-2 rounded-lg transition-all duration-200 ${user === $userId ? 'bg-orange-500 text-white ml-auto' : 'bg-gray-500 text-white mr-auto'}`}
	>
		<p class="break-words">
			{message}
		</p>
		<div class="text-xs text-right mt-1">
			{new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true })}
		</div>
	</div>
</div>




