<script>
	import { goto } from '$app/navigation';
	import { onMount } from 'svelte';

	let username = '';
	let password = '';
	let errorMessage = '';
	let isLoggedIn = false;

	onMount(() => {
		const token = localStorage.getItem('accessToken');
		if (token) {
			isLoggedIn = true;
			goto('/chat');
		}
	});

	async function login() {
		errorMessage = '';

		if (!username || !password) {
			errorMessage = 'Username and password are required!';
			return;
		}

		try {
			const response = await fetch('http://localhost/auth/login', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ username, password })
			});

			if (response.ok) {
				const data = await response.json();
				localStorage.setItem('accessToken', data.access_token);
				localStorage.setItem('username', username);
				goto('/chat');
			} else {
				const error = await response.json();
				errorMessage = error.message;
			}
		} catch (err) {
			errorMessage = 'An error occurred during login. Please try again.';
			console.error(err);
		}
	}


</script>

<div>
	<h1>Chat Application</h1>
	<h2>Login</h2>

	{#if isLoggedIn}
		<p>You are already logged in. Redirecting...</p>
	{:else}
		<form on:submit|preventDefault={login}>
			<div>
				<label for="username">Username:</label>
				<input id="username" bind:value={username} type="text" placeholder="Enter your username" required />
			</div>
			<div>
				<label for="password">Password:</label>
				<input id="password" bind:value={password} type="password" placeholder="Enter your password" required />
			</div>

			{#if errorMessage}
				<p style="color: red;">{errorMessage}</p>
			{/if}

			<button type="submit">Login</button>
		</form>
	{/if}
</div>

<style>
    div {
        margin-bottom: 15px;
    }

    label {
        display: block;
        margin-bottom: 5px;
    }

    input {
        padding: 8px;
        font-size: 16px;
        width: 100%;
        margin-bottom: 10px;
    }

    button {
        padding: 8px 16px;
        font-size: 16px;
        cursor: pointer;
    }
</style>
