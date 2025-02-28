export default interface UserAuth {
    id: number;
    userName?: string;
    email?: string;
    password: string;
    accessToken: string;
    refreshToken: string;
    roles: string[];
}