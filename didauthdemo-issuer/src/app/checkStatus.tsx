'use client'

import React from 'react';

async function checkAuthStatus(challenge:string) {
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

export default async function CheckStatus({challenge}) {

    return (<button className='btn btn-primary' onClick={async () => await checkAuthStatus(challenge)}>Check Auth Status</button>);
}