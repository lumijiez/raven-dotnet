<script>
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import "../app.css";

	const isLoggedIn = () => {
		return !!localStorage.getItem('access_token');
	};

	onMount(() => {
		if (!isLoggedIn() && !window.location.pathname.includes('/login')) {
			goto('/login');
		}
	});
</script>

<slot />