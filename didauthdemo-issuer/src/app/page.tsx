import React from 'react';
import QRCode from "react-qr-code";

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

async function checkAuthStatus() {
  const res = await fetch(`http://localhost:5196/api/check-auth-status/${challenge}`, { cache: 'no-store'});
  if(res.ok) {
    var resObj = await res.json();
    if(resObj.isAuthenticated) {
      console.log(resObj.accessToken);
      alert("Successfully Authenticated!")
    }else {
      alert("Not Authenticated")
    }
  }
}

export default async function Home() {
  const authRequest = await getAuthRequest();
  console.log("payload", authRequest);

  return (
    <div className="w-full p-6 m-auto rounded-md shadow-md lg:max-w-lg">
        <h1 className="text-3xl font-semibold text-center text-purple-700">DID Auth Demo</h1>
        <p className="text-center">Please scan the QR Code to with your mobile app to login.</p>
        <QRCode
          size={256}
          style={{ height: "500", maxWidth: "100%", width: "100%" }}
          value={authRequest}
          viewBox={`0 0 256 256`}
          />
          <button className='btn btn-primary' onClick={checkAuthStatus}>Check Auth Status</button>
    </div>
  )
}
