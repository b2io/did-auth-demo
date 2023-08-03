import React from 'react';
import AuthGuard from '@/components/authGuard';
import CredentialListPage from '@/components/credentialListPage';

export default async function Home() {
  return (
    <AuthGuard>
      <CredentialListPage />
    </AuthGuard>
  )
}
