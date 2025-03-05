/** The key should be provided without VITE_ prefix. */
export function getViteEnv(key: string) {
    const fullKey = "VITE_" + key;
    const envVar = import.meta.env[fullKey];
    if (!envVar) { 
        console.error(`No environment variable with a key ${fullKey} is found`);
    }
    return envVar;
}