import { JwtPayload } from "jwt-decode";

export default interface ExtendedJwtPayload extends JwtPayload {
    roles: string[];
}