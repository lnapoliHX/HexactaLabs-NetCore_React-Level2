import React from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";
import PropTypes from "prop-types";

const ElementRemove = ({ remove, goBack }) => {
  return (
    <Modal isOpen>
      <ModalHeader>Eliminar Producto</ModalHeader>
      <ModalBody>¿Esta seguro de eliminar este producto?</ModalBody>
      <ModalFooter>
        <Button color="danger" onClick={remove}>
          Si
        </Button>
        <Button color="secondary" onClick={goBack}>
          Necesito Pensarlo
        </Button>
      </ModalFooter>
    </Modal>
  );
};

ElementRemove.propTypes = {
  remove: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
};

ElementRemove.displayName = "ProductRemove";

export default ElementRemove;
