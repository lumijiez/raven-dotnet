<script>
	import { onMount } from 'svelte';
	import { addChatMessage } from '$lib/global.js';
	import ChatList from '$lib/components/ChatList.svelte';
	import ChatBox from '$lib/components/ChatBox.svelte';
	import * as signalR from '@microsoft/signalr';
	import { userId } from '$lib/global.js';
	import {
		ArrowRightToBracketOutline,
		CogOutline,
		EnvelopeOutline,
		HomeOutline,
		ImageOutline
	} from 'flowbite-svelte-icons';
	import { jwtDecode } from 'jwt-decode';
	import logo from '$lib/assets/ravenlogo.png'

	let selectedChatId = null;
	let connection;
	let token = '';

	onMount(async () => {
		const token = localStorage.getItem('accessToken');
		extractUserId(token);
		await getChatList(token);
		await connectToSignalR(token);
	});

	async function getChatList(token) {
		const response = await fetch('http://localhost/message/chat/list', {
			method: 'GET',
			headers: {
				'Authorization': `Bearer ${token}`
			}
		});

		if (response.ok) {
			const chatsData = await response.json();
			console.log(chatsData);
			chatsData.forEach(chat => {
				addChatMessage(chat.chatId, "LOAD FROM", "DATABASE");
			});
		} else {
			console.error('Error fetching chat list:', response.status);
		}
	}

	async function connectToSignalR(token) {
		connection = new signalR.HubConnectionBuilder()
			.withUrl('http://localhost/message', {
				accessTokenFactory: () => token
			})
			.build();

		connection.on('ReceiveMessage', (chatId, user, message) => {
			addChatMessage(chatId, user, message);
		});

		connection.on('ReceiveSystem', (message) => {
			addChatMessage("System", "System", message);
		})

		try {
			await connection.start();
			console.log('Connected to SignalR hub');
		} catch (err) {
			console.error('Error connecting to SignalR hub:', err);
		}
	}

	const extractUserId = (token) => {
		try {
			const decoded = jwtDecode(token);
			console.log(decoded.sub);
			$userId = decoded.sub;
		} catch (error) {
			console.log(error);
		}
	};
</script>

<div class="flex flex-col max-h-screen w-full h-screen">
	<div class="flex h-full">

		<div class="flex items-center flex-col justify-between h-screen w-16 bg-[#202938] border-r border-gray-700 text-white">
			<div class="flex flex-col items-center space-y-4 pt-4">

				<div class="flex items-center justify-center h-12 w-12 rounded-full">
					<img src={logo} alt="img" class="w-12 h-12 text-primary-700" />
				</div>

				<div class="flex items-center justify-center h-12 w-12 rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200">
					<HomeOutline class="w-8 h-8 text-primary-700" />
				</div>
				<div class="flex items-center justify-center h-12 w-12 rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200">
					<EnvelopeOutline class="w-8 h-8 text-primary-700" />
				</div>
				<div class="flex items-center justify-center h-12 w-12 rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200">
					<ImageOutline class="w-8 h-8 text-primary-700" />
				</div>
				<div class="flex items-center justify-center h-12 w-12 rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200">
					<ArrowRightToBracketOutline class="w-8 h-8 text-primary-700" />
				</div>
			</div>
			<div class="flex items-center justify-center h-12 w-12 rounded-full hover:bg-primary-400 hover:cursor-pointer transition duration-200 mb-4">
				<CogOutline class="w-8 h-8 text-primary-700" />
			</div>
		</div>

		<ChatList {connection} />
		<ChatBox {connection} />
	</div>
</div>
