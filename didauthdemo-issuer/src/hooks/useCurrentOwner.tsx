import { useEffect, useState } from "react";
import { CurrentUser } from "./useCurrentUser";
import jwtDecode from "jwt-decode";

export type Token = {
    unique_name: string
}

function createTokenInitializer() : Token | null {
    return null
}

export function useCurrentOwner(currentUser: CurrentUser | null) {
    const [token, setToken] = useState(createTokenInitializer);

    useEffect(() => {
        if(currentUser !== null) {
            if(currentUser.accessToken !== null) {
                const decodedToken : Token = jwtDecode(currentUser.accessToken);
                setToken(decodedToken);
            }
        }
      }, [currentUser]);

    return {token};
}