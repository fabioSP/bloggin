import { writable } from 'svelte/store';

const authenticated = {};

export const LoginStore = writable(authenticated);