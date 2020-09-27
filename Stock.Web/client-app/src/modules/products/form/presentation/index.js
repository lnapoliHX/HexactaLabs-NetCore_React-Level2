import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import schema from "../validation";
import InputField from "../../../../components/inputs/InputField";
import Validator from "../../../../common/helpers/YupValidator";
import ListSelect from "./ListSelect.js";

const ProductForm = (props) => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field
        label="Costo"
        name="costPrice"
        component={InputField}
        type="text"
      />
      <Field
        label="Precio"
        name="salePrice"
        component={InputField}
        type="text"
      />
      <ListSelect />
      <Field
        label="Proveedor"
        name="providerId"
        component={InputField}
        type="text"
      />
      <Button className="product-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="product-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired,
};

export default reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true,
})(ProductForm);
