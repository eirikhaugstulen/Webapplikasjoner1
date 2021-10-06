import React from "react";
import {Button, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";

export const SlettModal = ({ id, isOpen, onConfirm, onCancel }) => (
    <>
        <Modal isOpen={isOpen}>
            <ModalHeader>Kanseller billett?</ModalHeader>
            <ModalBody>
                <h3>Uh oh!</h3>
                <p className={'text-muted'}>Er du sikker på at du vil kansellere billetten din?</p>
            </ModalBody>
            <ModalFooter>
                <Button
                    color={'secondary'}
                    onClick={onCancel}
                >
                    Avbryt
                </Button>
                <Button
                    color={'danger'}
                    onClick={() => onConfirm(id)}
                >
                    Kanseller
                </Button>
            </ModalFooter>
        </Modal>
    </>
) 