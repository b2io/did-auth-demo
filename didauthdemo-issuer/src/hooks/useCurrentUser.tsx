"use client";

import { useEffect, useState } from "react";

export type CurrentUser = {
    accessToken: string | null
}

function createUserInitializer() : CurrentUser | null {
    return null
}

export function useCurrentUser() {
    const [currentUser, setCurrentUser] = useState(createUserInitializer);
    const [isLoadingUser, setIsLoadingUser] = useState(true);

    useEffect(() => {
        const data = window.localStorage.getItem('DID_AUTH_TOKEN');
        if ( data !== null ) setCurrentUser(JSON.parse(data));
        else setCurrentUser(null);
        setIsLoadingUser(false);
      }, []);

    useEffect(() => {
        if(currentUser !== null) window.localStorage.setItem('DID_AUTH_TOKEN', JSON.stringify(currentUser));
    }, [currentUser]);

    return {currentUser, isLoadingUser, setCurrentUser};
}