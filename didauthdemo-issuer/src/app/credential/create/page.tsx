import AuthGuard from "@/components/authGuard";
import PageTitle from "@/components/pageTitle";
import CreateCredentialForm from "./createCredentialForm";

export default function CreateCredentialPage() {
    return (
        <AuthGuard>
            <CreateCredentialForm />
        </AuthGuard>
    )
}