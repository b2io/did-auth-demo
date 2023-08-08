import SignIn from "@/app/sign-in/signIn";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { useState } from "react";

let challenge = "";

async function getAuthRequest() {
    const res = await fetch("http://localhost:5196/api/get-auth-request", { cache: 'no-store' });
    
    // Recommendation: handle errors
    if (!res.ok) {
      // This will activate the closest `error.js` Error Boundary
      throw new Error('Failed to fetch data')
    }
  
    const request = await res.json();
    challenge = request.challenge;
  
    return JSON.stringify(request);
}

export default async function SignInPage() {
    const authRequest = await getAuthRequest();
    const { setCurrentUser } = useCurrentUser();

    return (
        <SignIn challenge={challenge} 
                authRequest={authRequest} 
                setCurrentUser={setCurrentUser} />
    )
}