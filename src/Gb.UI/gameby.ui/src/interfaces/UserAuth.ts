export default interface UserAuth {
    id: number;
    username?: string;
    email?: string;
    accessToken: string;
    refreshToken: string;
    roles: string[];
}