export default function Modal({ setModalState, children }: { setModalState: any, children: React.ReactNode }) {
    return (
        <>
        <dialog id="my_modal_1" className="modal">
            <form method="dialog" className="modal-box">
                {children}
                {/* if there is a button in form, it will close the modal */}
                <button className="btn" onClick={() => setModalState(false)}>Close</button>
            </form>
        </dialog>
        </>
    );
}