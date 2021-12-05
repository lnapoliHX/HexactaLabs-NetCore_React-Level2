import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";
import SelectField from "../../../../components/inputs/SelectField";
import api from "../../../../common/api";
import { apiErrorToast } from "../../../../common/api/apiErrorToast";

const ProductForm = (props) => {
  const [productTypesState, dispatch] = useState([]);

  useEffect(() => {
    let callApi = true;

    api
      .get("/productType")
      .then((response) => {
        if (callApi) {
          return dispatch(response.data);
        }
      })
      .catch((error) => {
        apiErrorToast(error);
        callApi = false;
      });

    return () => {
      callApi = false;
    };
  }, []);

  const options = productTypesState.map((item) => {
    return { label: item.description, value: item.id };
  });

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
        label="Precio Venta"
        name="salePrice"
        component={InputField}
        type="text"
      />
      <Field
        label="Categoria"
        name="productTypeId"
        component={SelectField}
        options={options}
        valid={{ touched: false, error: false, pristine: false }}
        type="select"
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
