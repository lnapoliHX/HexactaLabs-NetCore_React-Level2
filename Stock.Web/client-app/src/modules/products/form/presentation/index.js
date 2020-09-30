import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";
import InputList from "./inputList";
const ProductForm = (props) => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field
        label="Precio Costo"
        name="costPrice"
        component={InputField}
        type="text"
      />
      <Field
        label="Precio Salida"
        name="salePrice"
        component={InputField}
        type="text"
      />
      <InputList
        url="/producttype"
        name="productTypeId"
        label="Categoria"
        param="description"
      />
      <InputList
        url="/provider"
        name="providerId"
        label="Proveedor"
        param="name"
      />

      <Button className="provider-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="provider-form__button"
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
