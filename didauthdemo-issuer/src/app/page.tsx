import React from 'react';
import AuthGuard from '@/components/authGuard';
import CredentialListPage from '@/app/credentialListPage';

export default async function Home() {
  return (
    <AuthGuard>
      <div className='container p-6 mx-auto my-3'>
        <CredentialListPage />  
      </div>
    </AuthGuard>
  )
}
