'use client'

import { useState, useEffect } from 'react';
import CredentialListPage from "./credentialListPage";
import SignIn from "./signIn";

// ...
export type CurrentUser = {
    accessToken: string | null
}

export type HomePageProps = {
    authRequest: any,
    challenge: string
}

export default function HomePage({authRequest, challenge}: HomePageProps) {

    
    /////////////////////////
    // better way to manage state? diff file?
    var defaultUser : CurrentUser = {accessToken: null};
    const [currentUser, setCurrentUser] = useState(defaultUser);
    const [isLoading, setIsLoading] = useState(true);

    //onload this function fires    
    useEffect(() => {
      const data = window.localStorage.getItem('DID_AUTH_TOKEN');
      if ( data !== null ) setCurrentUser(JSON.parse(data));
      setIsLoading(false);
    }, []);
  
    //when you call setCurrentUser this function fires
    useEffect(() => {
      window.localStorage.setItem('DID_AUTH_TOKEN', JSON.stringify(currentUser));
    }, [currentUser]);
    /////////////////////////

    if(isLoading)
        return (<></>)

    if(currentUser.accessToken !== null)
        return (<CredentialListPage />)
    
    return (
        <SignIn challenge={challenge} 
                authRequest={authRequest} 
                setCurrentUser={setCurrentUser} />
    )
}