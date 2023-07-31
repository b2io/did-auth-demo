import React from 'react';
import HomePage from '@/components/homepage';


/////////////////////////
//i dont like this being in this file
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
/////////////////////////

export default async function Home() {
  const authRequest = await getAuthRequest();
  console.log("payload", authRequest);

  return (
      <HomePage challenge={challenge} authRequest={authRequest} />
  )
}
